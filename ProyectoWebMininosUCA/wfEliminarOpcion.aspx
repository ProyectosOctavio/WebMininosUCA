<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEliminarOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarOpcion" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="banner-medium">
        <h1>Eliminar Opciones </h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar la opcion?</h1>
                    <div class="mininos-form">
   
        
   
    <asp:TextBox ID="txtIdOpcionEliminar" Visible="false" runat="server" Enabled="false"  />
   
    <div class="input-group">
    <asp:TextBox ID="txtAccionEliminar" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Accion"></asp:TextBox>
    <label id="lblAccionEliminar" for="txtAccionEliminar" class="label" runat="server">Accion</label>
    </div>

    <div class="input-group">
    <asp:TextBox ID="txtDescripcionEliminar" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Descripcion"></asp:TextBox>
    <label id="lblDescripcionElminar" for="txtDescripcionEliminar" class="label" runat="server">Descripcion</label>
    </div>                        

    <div class="input-group">
    <asp:TextBox ID="txtUrlEliminar" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="URL"></asp:TextBox>
    <label id="lblUrlEliminar" for="txtUrlEliminar" class="label" runat="server">URL</label>
    </div>
 

                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                            <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar Opcion" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
