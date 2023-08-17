<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfResolverMoneda.aspx.cs" Inherits="ProyectoWebMininosUCA.wfResolverMoneda" EnableEventValidation="false" EnableViewState="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="banner-medium">
        <h1>Resolver donacion monetaria</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea resolver la donacion?</h1>
                    <div class="mininos-form">
   
   
    
                        <asp:TextBox ID="txtIdMonedaResolver" Visible="false" runat="server" />

                         <asp:TextBox ID="txtIdDonanteResolver" Visible="false" runat="server"  />

                        <asp:TextBox ID="txtNombreResolver" Enabled="false" Class="mininos-input"  runat="server"  Placeholder="Nombres" />

                        <asp:TextBox ID="txtApellidoResolver" Enabled="false" Class="mininos-input"  runat="server" Placeholder="Apellidos"  />

                        <asp:TextBox ID="txtAliasResolver" Enabled="false" Class="mininos-input" runat="server" Placeholder="Alias"  />

                         <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>

                        <asp:TextBox ID="txtMontoResolver" Enabled="false"   runat="server" Placeholder="Monto" Class="mininos-input" />

                        
                       <asp:FileUpload ID="fuFotoResolver" runat="server" Enabled="false" Class="mininos-input" />

                                <asp:Image ID="imgFotoResolver" runat="server" Class="mininos-imagenes"  />






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
