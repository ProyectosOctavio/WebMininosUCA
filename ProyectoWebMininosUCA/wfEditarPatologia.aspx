<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEditarPatologia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarPatologia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
        <div class="banner-medium">
        <h1>Editar patología</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Editar una patología</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_patologiaEdit" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtDescripcionEdit" runat="server" Class="mininos-input" placeholder="Descripcion"/>
            </div>
        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEditarPatologia" runat="server"   Class="mininos-button-primary"  Text="Modificar patología" OnClick="btnEditarPatologia_Click"> </asp:Button>
                     <br />
                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
