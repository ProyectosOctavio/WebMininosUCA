<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfGestResidentes.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestResidentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="banner-medium">
        <h1>Registrar residente</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Registrar un residente</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtId_residente" Visible="false" runat="server" />

                        <div class="input-group">
                            <asp:FileUpload ID="fuResidente" runat="server" Class="mininos-input input-area" accept=".png,.jpg,.jpeg" />
                            <label id="lblFoto" runat="server" for="fuResidente" class="label">Foto ID</label>
                        </div>

                        <asp:Image ID="imgFoto" runat="server" CssClass="mininos-imagenes" />

                        <br />

                        <div class="input-group">
                            <asp:TextBox ID="txtNombre" runat="server" Class="mininos-input input-area"></asp:TextBox>
                            <label id="lblNombre" runat="server" for="txtNombre" class="label">Nombre</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtDescripcion" runat="server" Class="mininos-input input-area"></asp:TextBox>
                            <label id="lblDescripcion" runat="server" for="txtDescripcion" class="label">Descripcion</label>
                        </div>

                        <div class="date-selector">
                            <div class="input-group">
                                <input id="txtFechaNacimiento" type="date" runat="server" class="mininos-input input-area" />
                                <label id="lblFechaNacimiento" runat="server" for="txtFechaNacimiento" class="label">Fecha nacimiento</label>
                            </div>
                        </div>

                        <div class="input-group">
                            <asp:DropDownList ID="ddlSexo" title="Seleccionar Sexo" runat="server" Class="mininos-input input-area">
                                <asp:ListItem Text="Masculino" Value="false"></asp:ListItem>
                                <asp:ListItem Text="Femenino" Value="true"></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblSexo" runat="server" for="ddlSexo" class="label">Sexo</label>
                        </div>

                        <div class="input-group">
                            <asp:DropDownList ID="ddlEsterilizacion" title="Seleccionar estado de esterilizacion" runat="server" Class="mininos-input input-area">
                                <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblEsterilizacion" runat="server" for="ddlEsterilizacion" class="label">Esterilizado</label>
                        </div>

                        <div class="input-group">
                            <asp:DropDownList ID="ddlZona" title="Seleccionar zona" runat="server" Class="mininos-input input-area" />
                            <label id="lblZona" runat="server" for="ddlZona" class="label">Zona</label>
                        </div>

                        <div class="input-group">
                            <asp:CheckBoxList ID="cblPatologias" runat="server" class="chip-container" RepeatLayout="UnorderedList" />
                            <label id="lblPatogias" runat="server" for="cblPatologias" class="chip-container-label">Patologias</label>
                        </div>

                        <asp:Button ID="btnRegistrarResidente" runat="server" Class="mininos-button-primary" Text="Registrar nuevo residente" OnClick="btnGuardarResidente_Click"></asp:Button>
                        <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="limpiarCampos_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>
                    </div>
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
                    <div class="row">
                        <div class="col-full">
                            <div class="table-responsive">
                                <asp:GridView ID="gvResidente" runat="server" OnRowDataBound="gvResidente_RowDataBound" AllowPaging="true" OnPageIndexChanging="gv_Residente_PageIndexChanging"
                                    CssClass="table table-bordered table-condensed table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:Button runat="server" CssClass="btn btn-primary " Text="Editar" ID="btnEditarResidente" OnClick="btnEditarResidente_Click" />
                                                <asp:Button runat="server" CssClass="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarResidente" OnClick="btnEliminarResidente_Click" />
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
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        $(document).ready(function () {
            $('#<%= fuResidente.ClientID %>').change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#<%= imgFoto.ClientID %>').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(this.files[0]);
                }
            });
        });
    </script>

</asp:Content>

