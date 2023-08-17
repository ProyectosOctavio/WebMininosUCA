<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfEditarMoneda.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarMoneda" EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Editar donacion monetaria</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR DONACION MONETARIA</h1>
                    <div class="mininos-form">

                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtIdMonedaEdit" Visible="false" runat="server" />

                        <asp:TextBox ID="txtIdDonanteEdit" Visible="false" runat="server" />

                        <asp:TextBox ID="txtNombreEdit" Enabled="false" Class="mininos-input"  runat="server" />

                        <asp:TextBox ID="txtApellidoEdit" Enabled="false" Class="mininos-input"  runat="server" />

                        <asp:TextBox ID="txtAliasEdit" Enabled="false" Class="mininos-input" runat="server" />
                        
                        <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>

                        <asp:TextBox ID="txtMontoEdit" runat="server" Placeholder="Monto" Class="mininos-input" />

                        <div class="input-group">
                        <asp:FileUpload ID="fuFotoEdit" runat="server" CssClass="mininos-input input-area" />
                            <label id="lblSeleccion" for="fuFoto" class="label" runat="server">Seleccione una foto</label>
                            </div>

                        <div class="input-group">
                        <asp:Image ID="imgFotoEdit" runat="server" Class="mininos-imagenes" />
                            <label id="lblFoto" for="fuFoto" class="label" runat="server">Foto</label>
                        </div>


                        <strong class="font-weight-bold d-block" id="lblNota" runat="server">Si no aplica voucher, ingrese una captura de imagen en donde describa que fue lo que hizo</strong><br>

                        <div class="form-row-2">
           
                     
                    
                     </div>
                 

                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEditarMoneda" runat="server" OnClick="btnEditarMoneda_Click" Class="mininos-button-primary button" Text="Modificar Donacion"></asp:Button>
                    <br />
                    <br />

                </div>
            </div>
        </div>
    </div>

      </script>

       <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuFotoEdit.ClientID %>').change(function () {
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
