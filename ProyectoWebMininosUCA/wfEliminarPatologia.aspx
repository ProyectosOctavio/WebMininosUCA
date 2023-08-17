<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEliminarPatologia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarPatologia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
        <div class="banner-medium">
        <h1>Eliminar patología</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Eliminar una patología</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_patologiaEliminar" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtDescripcionEliminar" runat="server" Class="mininos-input" placeholder="Descripcion"/>
            </div>
        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEliminarPatologia" runat="server"   Class="mininos-button-primary"  Text="Eliminar patología" OnClick="btnEliminarPatologia_Click"> </asp:Button>
                     <br />
                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>