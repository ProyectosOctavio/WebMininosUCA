<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfGestionarOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfOpciones" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="banner-medium">
           <h1>GESTIONAR OPCIONES</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>AGREGAR OPCIONES</h1>
                    <div class="mininos-form">
                                  <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
                                   </div>
                        
                        <asp:TextBox ID="txtId" Visible="false" runat="server" />

                        <div class="input-group">
                        <asp:TextBox ID="txtAccion" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Accion"></asp:TextBox>
                        <label id="lblAccion" for="txtAccion" class="label" runat="server">Accion</label>
                        </div>

                        <div class="input-group">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Descripcion"></asp:TextBox>
                        <label id="lblDescripcion" for="txtDescripcion" class="label" runat="server">Descripcion</label>
                        </div>                        

                        <div class="input-group">
                        <asp:TextBox ID="txtUrl" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="URL"></asp:TextBox>
                        <label id="lblUrl" for="txtUrl" class="label" runat="server">URL</label>
                        </div>
                         

                        <br />
                        <br />
                        <br />

                    </div>
    <asp:Button ID="btnGuardarOpcion" runat="server"  OnClick="guardarOpcion_Click" Class="mininos-button-primary button"  Text="Guardar"></asp:Button>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnLimpiarCampos" runat="server"  OnClick="LimpiarOpcion_Click" Class="mininos-button-primary button"  Text="Limpiar"></asp:Button>                    <br />

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
                   
                        <div class="col-full">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOpcion" runat="server" OnRowDataBound="gvOpcion_RowDataBound"
                                    CssClass="table table-bordered table-condensed table-responsive table-hover ">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarOpcion" OnClick="btnEditarOpcion_Click" />
                                                <asp:Button runat="server" Class="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarOpcion" OnClick="btnEliminarOpcion_Click" />
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
        </div>
    

</asp:Content>
