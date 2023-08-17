<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfGestPublicaciones.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestPublicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Publicaciones</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                <h1>Gestionar Publicaciones</h1>
                <br />
                <a runat="server" href="~/wfAgregarPublicacion" class="mininos-button-secondary button">Agregar Publicación</a>                       
                <br />
                <asp:TextBox ID="txtIdPublicacion" Visible="false" runat="server" />

                <asp:FileUpload ID="archivoPublicacion" runat="server" Placeholder="Archivo" Class="mininos-input" Visible="false" />

                <asp:TextBox ID="txtTituloPublicacion" runat="server" Placeholder="Título" Class="mininos-input" Visible="false" />

                <asp:TextBox ID="txtTipoPublicacion" runat="server" Placeholder="Título" Class="mininos-input" Visible="false" />

                <asp:TextBox ID="txtContenido" runat="server" Placeholder="Título" Class="mininos-input" Visible="false" />
                    </div>
                <br />
                <br />
                <asp:Panel ID="pnlGuardarPublicacion" runat="server" CssClass="alert alert-warning" Visible="false">
                 <strong>Aviso: La Publicación ha sido Guardada.</strong>
                 <asp:Label ID="lblGuardar" runat="server"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlEditarPublicacion" runat="server" CssClass="alert alert-warning" Visible="false">
                 <strong>Aviso: La Publicación ha sido Editada.</strong>
                 <asp:Label ID="lblEditar" runat="server"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlEliminarPublicacion" runat="server" CssClass="alert alert-warning" Visible="false">
                 <strong>Aviso: La Publicación ha sido Eliminada.</strong>
                 <asp:Label ID="lblAdvertencia" runat="server"></asp:Label>
                </asp:Panel>
                <div class="form-row-2">

                    <asp:TextBox ID="txtBusqueda" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Class="mininos-button-secondary" />
                </div>
                <br />
                <br />
                <%--<div class="scrollable-gridview">--%>
                 <div class="row">
                  <div class="row">
                   <div class="col-full">
                  <div class="table-responsive">
                <asp:GridView ID="gvPublicaciones" runat="server" AutoGenerateColumns="true" OnRowDataBound="gvPublicaciones_RowDataBound"
                    AllowPaging="true" OnPageIndexChanging="gvPublicaciones_PageIndexChanging"
                    CssClass="table table-bordered table-condensed table-responsive table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="ACCIONES" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarPublicacion" OnClick="btnEditarPublicacion_Click" />
                                <asp:Button runat="server" Class="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarPublicacion" OnClick="btnEliminarPublicacion_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    </asp:GridView>
                 </div>
              </div>
            </div>
          </div>
        <%--</div>--%>
                
      </div>
    </div>
   </div>
</asp:Content>
