<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfGestionarMoneda.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestionMonetaria" EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner-medium">
        <h1>Donaciones Monetarias</h1>
    </div>

    <asp:TextBox ID="txtId" Visible="false" runat="server" />

    <asp:TextBox ID="txtNombre" Visible="false" runat="server" />

    <asp:TextBox ID="txtApellido" Visible="false" runat="server" />

    <asp:TextBox ID="txtAlias" Visible="false" runat="server" />

    <asp:TextBox ID="txtMonto" Visible="false" runat="server" />


    <asp:TextBox ID="txtVoucher" Visible="false" runat="server" />


    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <h1>Activas</h1>

                 <div class="form-row-2">
                      <br />
                <br />
                    <asp:TextBox ID="txtBusquedaActiva" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscarActiva" runat="server" Text="Buscar" OnClick="btnBuscarActiva_Click" Class="mininos-button-secondary" />
                      <br />
                <br />
                </div>

                <div class="mininos-form">
                    <div class="scrollable-gridview">
                    <asp:GridView ID="gvMonedaActiva" runat="server" OnRowDataBound="gvMoneda_RowDataBound" AllowPaging="true" OnPageIndexChanging="gv_Activa_PageIndexChanging"
                        CssClass="table table-bordered table-condensed table-responsive table-hover ">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button runat="server" Class="btn btn-primary btn-resolver" Text="Resolver" ID="btnResolverMoneda" OnClick="btnResolverMoneda_Click" />
                                    <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarMoneda" OnClick="btnEditarMoneda_Click" />
                                    <asp:Button runat="server" Class="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarMoneda" OnClick="btnEliminarMoneda_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#008080" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                        <RowStyle BackColor="#f5f5f5" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />

                    </asp:GridView>

                </div>


            </div>
        </div>
          </div>
        <hr />


        <div class="row">
            <div class="col-full">
                <h1 class="hi">Historial</h1>

                 <div class="form-row-2">
                      <br />
                <br />
                    <asp:TextBox ID="txtBuscarHistorial" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscarHistorial" runat="server" Text="Buscar" OnClick="btnBuscarHistorial_Click" Class="mininos-button-secondary" />
                      <br />
                <br />
                </div>

                <div class="mininos-form">
                    <div class="scrollable-gridview">
                    <asp:GridView ID="gvMonedaHistorial" runat="server" OnRowDataBound="gvMonedaH_RowDataBound" AllowPaging="true" OnPageIndexChanging="gv_Historial_PageIndexChanging"
                          CssClass="table table-bordered table-condensed table-responsive table-hover ">
                        <Columns>
                        </Columns>
                         <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor=" #008080" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                        <RowStyle BackColor="#f5f5f5" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />

                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
     </div>
</asp:Content>

