<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfResolverEspecia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfResolverEspecia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner-medium">
        <h1>Resolver donacion de especias</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea resolver la donacion?</h1>
                    <div class="mininos-form">
   
   
    
                        <asp:TextBox ID="txtIdResolver"  Visible="false"   runat="server" />

                              <asp:TextBox ID="txtIdDonanteResolver" Visible="false"  runat="server" />

                            <asp:TextBox ID="txtNombreResolver" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtApellidoResolver" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtAliasResolver" Enabled="false" Class="mininos-input" runat="server" />

                        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>

                            <asp:TextBox ID="txtCantidadResolver" Enabled="false" Class="mininos-input"  Placeholder="Cantidad" runat="server" />

                        <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                            <asp:TextBox ID="txtTipoEspecieResolver"  Enabled="false" Class="mininos-input"  Placeholder="Tipo" runat="server" />

                         <asp:Label ID="lblMedida" runat="server" Text="Medida"></asp:Label>
                            <asp:TextBox ID="txtUnidadMedidaResolver" Enabled="false" Class="mininos-input"  Placeholder="Medida"  runat="server" />





                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnResolver" runat="server" OnClick="btnResolver_Click" Text="Resolver donacion" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
