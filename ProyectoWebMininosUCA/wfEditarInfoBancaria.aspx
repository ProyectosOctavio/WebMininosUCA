<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarInfoBancaria.aspx.cs" EnableEventValidation="false" Inherits="ProyectoWebMininosUCA.wfEditarInfoBancaria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css">

  
        <div class="banner-medium">
        <h1>Informacion bancaria</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Editar informacion bancaria</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_InfoBancariaEdit" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtBancoEdit" runat="server" Class="mininos-input" placeholder="Banco"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtNumeroCuentaEdit" runat="server" Class="mininos-input" placeholder="Numero cuenta"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtMonedaEdit" runat="server" Class="mininos-input" placeholder="Moneda"/>
            </div>
<asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEditarInfoBancaria" runat="server"   Class="mininos-button-primary"  Text="Modificar Info Bancaria" OnClick="btnEditarInfoBancaria_Click"> </asp:Button>
                     <br />
     
            </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
