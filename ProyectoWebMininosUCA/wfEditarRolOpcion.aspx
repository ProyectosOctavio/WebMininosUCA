<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarRolOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarRolOpcion"  EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner-medium">
        <h1>EDITAR ROL OPCION</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR ROL OPCION</h1>
                    <div class="mininos-form">

                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>

                        
                         <asp:TextBox ID="txtIdEdit" Visible="false" runat="server" />          

                         <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>
                                              
                        <asp:DropDownList ID="ddlRolEdit" runat="server" Class="mininos-input" placeholder="Roles">
                           
                        </asp:DropDownList>

                        <asp:Label ID="lblOpcion" runat="server" Text="Opcion"></asp:Label>

                        <asp:DropDownList ID="ddlOpcionEdit" runat="server" Class="mininos-input" placeholder="Opciones">
                           
                        </asp:DropDownList>



                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEditarRolOpcion" runat="server" OnClick="btnEditarRolOpcion_Click" Class="mininos-button-primary button" Text="Modificar Rol Opcion"></asp:Button>
                      <br />
                        
                     <br/>
                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />

                </div>
            </div>
        </div>
    </div>

</asp:Content>
