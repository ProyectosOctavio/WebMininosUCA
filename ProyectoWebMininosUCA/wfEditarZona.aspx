<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEditarZona.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarZona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
  
        <div class="banner-medium">
        <h1>Editar Zona</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Editar una Zona</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_ZonaEdit" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtNombreEdit" runat="server" Class="mininos-input" placeholder="Nombre"/>
            </div>
        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEditarZona" runat="server"   Class="mininos-button-primary"  Text="Modificar Zona" OnClick="btnEditarZona_Click"> </asp:Button>
                     <br />
                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
</asp:Content>
