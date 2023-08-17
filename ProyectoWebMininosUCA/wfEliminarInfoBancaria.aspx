<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEliminarInfoBancaria.aspx.cs" EnableEventValidation="false" Inherits="ProyectoWebMininosUCA.wfEliminarInfoBancaria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css">

  
        <div class="banner-medium">
        <h1>Informacion bancaria</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Eliminar informacion bancaria</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_InfoBancariaEliminar" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtBancoEliminar" runat="server" Class="mininos-input" placeholder="Banco"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtNumeroCuentaEliminar" runat="server" Class="mininos-input" placeholder="Numero cuenta"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtMonedaEliminar" runat="server" Class="mininos-input" placeholder="Moneda"/>
            </div>
<asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEliminarInfoBancaria" runat="server"   Class="mininos-button-primary"  Text="Eliminar Info Bancaria" OnClick="btnEliminarInfoBancaria_Click"> </asp:Button>
                     <br />
     
            </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
