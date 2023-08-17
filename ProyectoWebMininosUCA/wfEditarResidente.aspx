<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEditarResidente.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarResidente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner-medium">
        <h1>Editar residente</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Editar un residente</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:TextBox ID="txtId_residenteEdit" Visible="false" runat="server" />

                        <asp:TextBox ID="txtNombreEdit" runat="server" Class="mininos-input" placeholder="Nombre" />

                        <asp:TextBox ID="txtDescripcionEdit" runat="server" Class="mininos-input" placeholder="Descripcion" />
                        <div class="form-row-2">
                            <asp:Label runat="server">Fecha nacimiento:</asp:Label>
                            <div class="date-selector">
                                <input type="date" id="txtFechaNacimientoEdit" runat="server" class="mininos-input" />
                            </div>
                        </div>
                        <div class="form-row-2">
                            <asp:Label runat="server">Fecha desaparición:</asp:Label>
                            <div class="date-selector">
                                <input type="date" id="txtFechaDesaparicionEdit" runat="server" class="mininos-input" />
                            </div>
                        </div>
                        <div class="form-row-2">
                            <asp:Label runat="server">Fecha defunción:</asp:Label>
                            <div class="date-selector">
                                <input type="date" id="txtFechaDefuncionEdit" runat="server" class="mininos-input" />
                            </div>
                        </div>
                        <asp:DropDownList ID="ddlSexoEdit" runat="server" Class="mininos-input">
                            <asp:ListItem Text="Sexo" Value=""></asp:ListItem>
                            <asp:ListItem Text="Masculino" Value="false"></asp:ListItem>
                            <asp:ListItem Text="Femenino" Value="true"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlEsterilizacionEdit" runat="server" Class="mininos-input">
                            <asp:ListItem Text="Esterilizacion" Value=""></asp:ListItem>
                            <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                            <asp:ListItem Text="No" Value="false"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:Label runat="server">Patologias</asp:Label>
                        <asp:CheckBoxList ID="cblPatologiasEditar" runat="server" class="chip-container" RepeatLayout="UnorderedList">
                        </asp:CheckBoxList>
                        <asp:Label ID="lblMensajePatologiasEditar" runat="server" Visible="false" CssClass="message-label"></asp:Label>



                        <asp:Label runat="server">Seleccionar zona:</asp:Label>

                        <asp:DropDownList ID="ddlZonaEdit" runat="server" Class="mininos-input">
                            <asp:ListItem Text="Zona" Value=""></asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <asp:Label runat="server">Subir foto:</asp:Label>
                        <asp:FileUpload ID="fuResidenteEdit" runat="server" Class="mininos-input" />
                        <asp:Image ID="imgFotoEdit" runat="server" CssClass="mininos-imagenes" />
                        <br />
                    </div>


                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                    <br />
                    <br />
                    <asp:Button ID="btnEditarResidente" runat="server" Class="mininos-button-primary" Text="Modificar residente" OnClick="btnEditarResidente_Click"></asp:Button>
                    <br />


                </div>
            </div>
        </div>


    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuResidenteEdit.ClientID %>').change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#<%= imgFotoEdit.ClientID %>').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(this.files[0]);
                }
            });
        });
    </script>

</asp:Content>
