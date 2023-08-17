<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEditarResidentePatologia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarResidentePatologia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
        <div class="banner-medium">
        <h1>Editar residente patología</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Editar patología de un residente</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_residentePatologiaEdit" Visible="false" runat="server" />
                                                  <asp:Label runat="server">Seleccionar residente:</asp:Label>

             <asp:DropDownList ID="ddlResidenteIdEdit" runat="server" Class="mininos-input">
                 <asp:ListItem Text="Residente" Value=""></asp:ListItem>
             </asp:DropDownList>
                                                  <asp:Label runat="server">Seleccionar patología:</asp:Label>

                <asp:DropDownList ID="ddlPatologiaIdEdit" runat="server" Class="mininos-input">
                 <asp:ListItem Text="Patologia" Value=""></asp:ListItem>
             </asp:DropDownList>

        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEditarResidentePatologia" runat="server"   Class="mininos-button-primary"  Text="Editar patología de un residente" OnClick="btnEditarResidentePatologia_Click"> </asp:Button>
                     <br />

                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
