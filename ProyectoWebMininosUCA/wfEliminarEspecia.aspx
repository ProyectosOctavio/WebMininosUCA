<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfEliminarEspecia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarEspecia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="banner-medium">
        <h1>Eliminar donacion de especia</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar la donacion?</h1>
                    <div class="mininos-form">
   
   
    
                        
                            <asp:TextBox ID="txtIdEliminar"  Visible="false"   runat="server" />

                              <asp:TextBox ID="txtIdDonanteEliminar" Visible="false"  runat="server" />

                            <asp:TextBox ID="txtNombreEliminar" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtApellidoEliminar" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtAliasEliminar" Enabled="false" Class="mininos-input" runat="server" />

                           <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
                            <asp:TextBox ID="txtCantidadEliminar" Enabled="false" Class="mininos-input"  Placeholder="Cantidad" runat="server" />

                             <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                            <asp:TextBox ID="txtTipoEspecieEliminar"  Enabled="false" Class="mininos-input"  Placeholder="Tipo" runat="server" />

                         <asp:Label ID="lblMedida" runat="server" Text="Medida"></asp:Label>
                            <asp:TextBox ID="txtUnidadMedidaEliminar" Enabled="false" Class="mininos-input"  Placeholder="Medida"  runat="server" />





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
