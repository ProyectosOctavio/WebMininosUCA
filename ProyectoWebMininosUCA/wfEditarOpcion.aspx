<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarOpcion" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="banner-medium">
        <h1>Editar Opciones</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR OPCIONES</h1>
                    <div class="mininos-form">
             <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
                                   </div>
        
                        <asp:TextBox ID="txtIdOpcionEdit" Visible="false" runat="server" />

                        <div class="input-group">
                        <asp:TextBox ID="txtAccionEdit" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Accion"></asp:TextBox>
                        <label id="lblAccionEdit" for="txtAccionEdit" class="label" runat="server">Accion</label>
                        </div>

                        <div class="input-group">
                        <asp:TextBox ID="txtDescripcionEdit" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Descripcion"></asp:TextBox>
                        <label id="lblDescripcionEdit" for="txtDescripcionEdit" class="label" runat="server">Descripcion</label>
                        </div>                        

                        <div class="input-group">
                        <asp:TextBox ID="txtUrlEdit" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="URL"></asp:TextBox>
                        <label id="lblUrlEdit" for="txtUrlEdit" class="label" runat="server">URL</label>
                        </div>
                                                 

                        <br />
                        <br />
                        <br />

 </div>
    <asp:Button ID="btnEditarOpcion" runat="server"  OnClick="btnEditarOpcion_Click" Class="mininos-button-primary button"  Text="Modificar Opcion"></asp:Button>
                    <br/>
                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
