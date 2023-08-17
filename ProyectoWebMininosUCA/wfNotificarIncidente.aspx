<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfNotificarIncidente.aspx.cs" Inherits="ProyectoWebMininosUCA.GestionarDonaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Registrar Incidentes</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Reportar un incidente</h1>
                    <div class="mininos-form">
                        
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                    
                        <div class="input-group">
                            <asp:TextBox ID="txtDescripcion" runat="server" Placeholder="Descripcion" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" />
                            <label id="lblDescripcion" for="txtDescripcion" class="label" runat="server">Descripcion</label>
                        </div>

                        <div class="input-group">
                            <asp:DropDownList ID="ddlNivelRiesgo" runat="server" Class="mininos-input input-area" placeholder="Nivel de Riesgo">
                                <asp:ListItem Text="Nivel de Riesgo" Value="" ></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblNivelRiesgo" for="ddlNivelRiesgo" class="label" runat="server">Nivel de Riesgo</label>
                        </div>

                        <div class="input-group">
                            <asp:FileUpload ID="fuFoto" runat="server" CssClass="mininos-input input-area" />
                            <label id="lblSeleccion" for="fuFoto" class="label" runat="server">Seleccione una foto</label>
                        </div>

                        <div class="input-group">
                            <asp:Image ID="imgFoto" runat="server" Class="mininos-imagenes" />
                            <label id="lblFoto" for="fuFoto" class="label" runat="server">Foto</label>
                        </div>

                        <div class="switch-container">
                            <label id="lblSeleccionarResidente" runat="server" class="switch-container-label">¿Desea seleccionar un gato?</label>
                            <label class="switch">
                                <asp:CheckBox ID="chkSeleccionarResidente" runat="server" OnCheckedChanged="chkSeleccionarResidente_CheckedChanged" AutoPostBack="true" />
                                <span class="slider round"></span>
                            </label>
                        </div>

                        <div runat="server" id="divGatoAfectado" visible="false">
                            <div class="input-group">
                                <asp:CheckBoxList ID="cblMininos" runat="server" SelectionMode="Single" class="chip-container" RepeatLayout="UnorderedList" />
                                <label id="lblMininos" runat="server" for="cblMininos" class="chip-container-label">Selecciona un gato afectado</label>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                    <asp:Button ID="btnGuardar" runat="server" OnClick="guardarIncidente_Click" Class="mininos-button-primary button" Text="Registrar Incidente"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuFoto.ClientID %>').change(function () {
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
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
    <script>
        function clearPlaceholder(element) {
            element.placeholder = '';
        }

        function restorePlaceholder(element) {
            element.placeholder = 'Descripcion';
        }
    </script>

</asp:Content>
