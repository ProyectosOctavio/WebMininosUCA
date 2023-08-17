<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfResolverApadrinamiento.aspx.cs" Inherits="ProyectoWebMininosUCA.wfResolverApadrinamiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="banner-medium">
        <h1>Resolver el apadrinamiento</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea resolver la apadrinacion?</h1>
                    <div class="mininos-form">
   
   
    
                        
     <asp:TextBox ID="txtId" Visible="false" runat="server" />

     <asp:TextBox ID="txtDonanteId" Visible="false" runat="server"  />

     <asp:TextBox ID="txtResidenteId" Visible="false" runat="server"  />

 <asp:TextBox ID="txtNombreDonante"  Enabled="false" Class="mininos-input" runat="server"  />

     <asp:TextBox ID="txtApellido"  Enabled="false" Class="mininos-input" runat="server"  />
    
     <asp:TextBox ID="txtAliasDonante"  Enabled="false" Class="mininos-input" runat="server"  />
    


        <asp:TextBox ID="txtResidente"  Enabled="false" Class="mininos-input" runat="server"  />




                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnResolver_Click" Text="Resolver apadrinacion" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>


