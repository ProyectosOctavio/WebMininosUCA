<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfDonarDonante.aspx.cs" Inherits="ProyectoWebMininosUCA.wfDonarDonante" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Donar</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">

                <h1>¿Por qué donar?</h1>
                <p>
                    Mininos UCA es una organización sin fines de lucro dedicada al rescate y cuidado de gatos en situación de calle.
                    Nuestro objetivo es darles una mejor calidad de vida y encontrarles hogares amorosos. Con tu ayuda podemos seguir salvando vidas felinas. 
                    ¡Únete a nuestra causa! ¡Ayúdanos a ayudar! Puedes hacer una donación para apoyar nuestra labor.
                </p>

                 

                <div class="center">
                    <img src="images/minino_template1.png" alt="logo" />
                </div>



                <div class="mininos-form-container">
                    <h1>Hacer una Donación</h1>

                    <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                        <strong>Aviso:</strong>
                        <span id="lblAdvertencia" runat="server"></span>
                    </div>

                    <div class="mininos-form">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlFormaDonacion" runat="server" Class="mininos-input  input-area" OnSelectedIndexChanged="ddlFormaDonacion_SelectedIndexChanged" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder8(this)" AutoPostBack="true">
                                <asp:ListItem Text="Forma de donacion" Value=""></asp:ListItem>
                                <asp:ListItem Text="Reconocido" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Anonimo" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlFormaDonacion" class="label">Forma de donacion</label>
                        </div>

                        <asp:TextBox ID="txtIdDonante" Visible="false" runat="server" />
                        <fieldset>
                            <legend>Datos personales</legend>
                        </fieldset>

                        <div class="form-row-3">
                            
                            <div class="input-group">
                                <asp:TextBox ID="txtCorreoDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Correo"></asp:TextBox>
                                <label id="lblCorreoDonante" for="txtCorreoDonante" class="label" runat="server">Correo</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtNombreDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder2(this)" Placeholder="Nombres"></asp:TextBox>
                                <label id="lblNombreDonante" for="txtNombreDonante" class="label" runat="server">Nombres</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtApellidoDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" Placeholder="Apellidos"></asp:TextBox>
                                <label id="lblApellidoDonante" for="txtApellidoDonante" class="label" runat="server">Apellidos</label>
                            </div>
                        </div>

                        <div class="form-row-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtAliasDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" Placeholder="Alias"></asp:TextBox>
                                <label id="lblAliasDonante" for="txtAliasDonante" class="label" runat="server">Alias</label>
                            </div>
                            <div class="input-group">
                                <asp:TextBox ID="txtTelefonoDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" Placeholder="Telefono"></asp:TextBox>
                                <label id="lblTelefonoDonante" for="txtTelefonoDonante" class="label" runat="server">Teléfono</label>
                            </div>

                        </div>

                        <div class="form-row-2">

                            <div class="input-group">
                                <asp:TextBox ID="txtPaisDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" Placeholder="Pais"></asp:TextBox>
                                <label id="lblPaisDonante" for="txtPaisDonante" class="label" runat="server">Pais</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtCiudadDonante" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" Placeholder="Ciudad"></asp:TextBox>
                                <label id="lblCiudadDonante" for="txtCiudadDonante" class="label" runat="server">Ciudad</label>

                            </div>
                        </div>
                     
                        <fieldset>
                                <legend>Detalles de la donacion</legend>
                                   </fieldset>

                        <div class="form-row-2">

                            <div class="input-group">
                                <asp:DropDownList ID="ddlTipoDonacion" runat="server" Class="mininos-input input-area" OnSelectedIndexChanged="ddlTipoDonacion_SelectedIndexChanged" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder9(this)" AutoPostBack="true">
                                    <asp:ListItem Text="Tipo de Donacion" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Monetaria" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Especia" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                                <label id="lblTipoDonacion" runat="server" for="ddlTipoDonacion" class="label">Tipo de Donacion</label>
                            </div>
                        </div>

                        <div class="form-row-3">

                            <div class="input-group">
                                <asp:DropDownList ID="ddlNombreDonacionEspecias" runat="server" Class="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder11(this)">
                                    <asp:ListItem Text="Tipo" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Comida en granos" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Comida humeda" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Comida en lata" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblNombreEspecias" runat="server" for="txtNombreDonacionEspecias" class="label">Tipo</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtCantDonacionEspecias" Placeholder="Cantidad" runat="server" Class="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder11(this)" />
                                <label id="lblCantidadEspecias" runat="server" for="txtCantDonacionEspecias" class="label">Cantidad de especias</label>
                            </div>

                            <div class="input-group">

                                <asp:DropDownList ID="ddlMedidaDonacion" runat="server" Class="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder12(this)">
                                    <asp:ListItem Text="Medida" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Libras" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Bolsas" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Bolsitas" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Sobres" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Latas" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblMedidaEspecias" runat="server" for="ddlMedidaDonacion" class="label">Medida</label>
                            </div>
                        </div>

                        <div class="form-row-2">

                            <div class="input-group">
                                <asp:TextBox ID="txtCantDonacionMonetaria" Placeholder="C$" runat="server" Class="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder13(this)" />
                                <label id="lblDonacionMonto" runat="server" for="txtCantDonacionMonetaria" class="label">Monto C$</label>
                            </div>



                        </div>
                        <p>
                            <strong class="font-weight-bold d-block" id="lblInfoBancaria" runat="server">Información de pago:</strong><br>

                            <asp:GridView ID="gvInfoBancaria" runat="server" OnRowDataBound="gvInfoBancaria_RowDataBound"
                                CssClass="table table-bordered table-condensed table-responsive table-hover ">
                                <Columns>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                                <RowStyle BackColor="#f5f5f5" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                            </asp:GridView>
                        </p>


                          <p>
                            <strong class="font-weight-bold d-block" id="lblNota" runat="server">Si no aplica voucher, ingrese una captura de imagen en donde describa que fue lo que hizo </strong><br>
</p>
                          <div class="input-group">
                            <asp:FileUpload ID="fuFoto" runat="server" CssClass="mininos-input input-area" />
                            <label id="lblSeleccion" for="fuFoto" class="label" runat="server">Agregue la imagen del voucher</label>
                        </div>

                         <div class="input-group">
                            <asp:Image ID="imgFoto" runat="server" Class="mininos-imagenes" />
                            <label id="lblFoto" for="fuFoto" class="label" runat="server">Foto</label>
                        </div>


                        <h1>¿Te gustaria apadrinar un gatito?</h1>
                        <div class="scrollable-checkboxlist">
                            <div class="form-row-2">
                                <asp:CheckBoxList ID="cblApadrinacion" runat="server" class="mininos-input">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <p>
                            Si le interesa aprender más sobre los adorables gatos, puede explorar nuestra galería.
                        </p>



                    </div>

                    <asp:Button ID="btnDonar" runat="server" Class="mininos-button-primary" Text="Donar" OnClick="btnDonar_Click" ></asp:Button>
                </div>

            </div>
        </div>
    </div>

    <script>
        function clearPlaceholder(element) {
            element.placeholder = '';
        }

        function restorePlaceholder(element) {
            element.placeholder = 'Alias';
        }

        function restorePlaceholder2(element) {
            element.placeholder = 'Nombres';
        }

        function restorePlaceholder3(element) {
            element.placeholder = 'Apellidos';
        }

        function restorePlaceholder4(element) {
            element.placeholder = 'País ';
        }

        function restorePlaceholder5(element) {
            element.placeholder = 'Ciudad';
        }

        function restorePlaceholder6(element) {
            element.placeholder = 'Teléfono';
        }

        function restorePlaceholder7(element) {
            element.placeholder = 'Correo';
        }

        function restorePlaceholder8(element) {
            element.placeholder = 'Forma de donacion';
        }

        function restorePlaceholder9(element) {
            element.placeholder = 'Tipo de donacion';
        }

        function restorePlaceholder10(element) {
            element.placeholder = 'Tipo';
        }

        function restorePlaceholder11(element) {
            element.placeholder = 'Cantidad';
        }

        function restorePlaceholder12(element) {
            element.placeholder = 'Medida';
        }

        function restorePlaceholder13(element) {
            element.placeholder = 'Monto';
        }
        function restorePlaceholder14(element) {
            element.placeholder = 'Moneda';
        }

       
    </script>

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



</asp:Content>
