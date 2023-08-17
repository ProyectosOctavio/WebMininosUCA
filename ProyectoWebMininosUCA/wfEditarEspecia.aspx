<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfEditarEspecia.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarEspecia" EnableEventValidation="false" EnableViewState="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="banner-medium">
            <h1>Editar donacion especia</h1>
        </div>
        <div class="content-container body-content">
            <div class="row">
                <div class="col-full">
                    <div class="mininos-form-container">
                        <h1>EDITAR DONACION ESPECIA</h1>
                        <div class="mininos-form">

                            <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                                <strong>Aviso:</strong>
                                <span id="lblAdvertencia" runat="server"></span>
                            </div>

                            <asp:TextBox ID="txtIdEdit"  Visible="false"   runat="server" />

                              <asp:TextBox ID="txtIdDonanteEdit" Visible="false"  runat="server" />

                            <asp:TextBox ID="txtNombreEdit" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtApellidoEdit" Enabled="false" Class="mininos-input"  runat="server" />

                            <asp:TextBox ID="txtAliasEdit" Enabled="false" Class="mininos-input" runat="server" />

                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
                            <asp:TextBox ID="txtCantidadEdit" Class="mininos-input"  Placeholder="Cantidad" runat="server" />

                             <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                            <asp:TextBox ID="txtTipoEspecieEdit"  Class="mininos-input"  Placeholder="Tipo" runat="server" />

                            <asp:Label ID="lblMedida" runat="server" Text="Medida"></asp:Label>
                            <asp:TextBox ID="txtUnidadMedidaEdit"  Class="mininos-input"  Placeholder="Medida"  runat="server" />


                            <br />
                            <br />
                            <br />

                        </div>
                        <asp:Button ID="btnEditarEspecia" runat="server" OnClick="btnEditarEspecia_Click" Class="mininos-button-primary button" Text="Modificar Donacion"></asp:Button>
                        <br />
                        <br />

                    </div>
                </div>
            </div>
        </div>
    </asp:Content>
