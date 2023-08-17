<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEliminarResidentePatologia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarResidentePatologia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
        <div class="banner-medium">
        <h1>Eliminar residente patología</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Eliminar patología de un residente</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_residentePatologiaEliminar" Visible="false" runat="server" />
                                                  <asp:Label runat="server">Seleccionar residente:</asp:Label>

             <asp:DropDownList ID="ddlResidenteIdEliminar" runat="server" Class="mininos-input">
                 <asp:ListItem Text="Residente" Value=""></asp:ListItem>
             </asp:DropDownList>
                                                  <asp:Label runat="server">Seleccionar patología:</asp:Label>

                <asp:DropDownList ID="ddlPatologiaIdEliminar" runat="server" Class="mininos-input">
                 <asp:ListItem Text="Patologia" Value=""></asp:ListItem>
             </asp:DropDownList>

        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEliminarResidentePatologia" runat="server"   Class="mininos-button-primary"  Text="Eliminar patología de un residente" OnClick="btnEliminarResidentePatologia_Click"> </asp:Button>
                     <br />

                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
