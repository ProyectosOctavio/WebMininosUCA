<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfGestResidentePatologia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestResidentePatologia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="banner-medium">
        <h1>Agregar residente patología</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Agregar patología a un residente</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:TextBox ID="txtId_residentePatologia" Visible="false" runat="server" />
                        <asp:Label runat="server">Seleccionar residente:</asp:Label>

                        <asp:DropDownList ID="ddlResidenteId" runat="server" Class="mininos-input">
                            <asp:ListItem Text="Residente" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server">Seleccionar patología:</asp:Label>

                        <asp:DropDownList ID="ddlPatologiaId" runat="server" Class="mininos-input">
                            <asp:ListItem Text="Patologia" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <asp:Button ID="btnRegistrarResidentePatologia" runat="server" Class="mininos-button-primary" Text="Agregar patología a un residente" OnClick="btnGuardarResidentePatologia_Click"></asp:Button>
                    <br />
                    <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="limpiarCampos_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>
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
                <div class="scrollable-gridview">
                    <div class="row">
                        <div class="row">
                            <div class="col-full">
                                <div class="table-responsive">

                                    <br />


                                    <asp:GridView ID="gvResidentePatologia" runat="server" OnRowDataBound="gvResidentePatologia_RowDataBound"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover ">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" CssClass="btn btn-primary " Text="Editar" ID="btnEditarResidentePatologia" OnClick="btnEditarResidentePatologia_Click" />
                                                    <asp:Button runat="server" CssClass="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarResidentePatologia" OnClick="btnEliminarResidentePatologia_Click" />
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


        </div>
    </div>
</asp:Content>
