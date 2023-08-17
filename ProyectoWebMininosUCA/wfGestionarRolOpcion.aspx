<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfGestionarRolOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestionarRolOpcion" EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="banner-medium">
        <h1>ROL OPCION</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>ASIGNAR OPCIONES</h1>
                    <div class="mininos-form">

                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>



                        <asp:TextBox ID="txtId" Visible="false" runat="server" />          

                         <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>
                                              
                        <asp:DropDownList ID="ddlRol" runat="server" Class="mininos-input" placeholder="Roles">
                           
                        </asp:DropDownList>

                        <asp:Label ID="lblOpcion" runat="server" Text="Opcion"></asp:Label>

                        <asp:DropDownList ID="ddlOpcion" runat="server" Class="mininos-input" placeholder="Opciones">
                           
                        </asp:DropDownList>


                        
                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnGuardarRolOpcion" runat="server" OnClick="guardarRolOpcion_Click" Class="mininos-button-primary button" Text="Guardar"></asp:Button>

                   

         <br />
                    <br />
                    <br />

                </div>

                <br />
                <br />
                <br />

                <div class="form-row-2">

                    <asp:TextBox ID="txtBusqueda" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                      <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Class="mininos-button-secondary" />
                    
                </div>

                <br />
                <br />
                <br />

                <div class="row">


                    <div class="col-half">

                        <div class="mininos-form">

                            <asp:GridView ID="gvRolOpcion" runat="server" OnRowDataBound="gvRolOpcion_RowDataBound"
                                CssClass="table table-bordered table-condensed table-responsive table-hover ">
                                <Columns>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarRolOpcion" OnClick="btnEditarRolOpcion_Click" />
                                            <asp:Button runat="server" Class="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarRolOpcion" OnClick="btnEliminarRolOpcion_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                 <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor=" #01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                                    <RowStyle BackColor="#f5f5f5" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                            </asp:GridView>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    
</asp:Content>
