<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfEditarApadrinamiento.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarApadrinamiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner-medium">
        <h1>Editar el apadrinamiento</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea editar la apadrinacion?</h1>
                    <div class="mininos-form">




                        <asp:TextBox ID="txtId" Visible="false" runat="server" />

                        <asp:TextBox ID="txtDonanteId" Visible="false" runat="server" />

                        <asp:TextBox ID="txtResidenteId" Visible="false" runat="server" />


                        <asp:TextBox ID="txtNombreDonante" Enabled="false" Class="mininos-input" runat="server" />

                        <asp:TextBox ID="txtApellido" Enabled="false" Class="mininos-input" runat="server" />

                        <asp:TextBox ID="txtAliasDonante" Enabled="false" Class="mininos-input" runat="server" />

                        <asp:Label ID="lblResidente" runat="server" Text="Residente"></asp:Label>

                        <asp:DropDownList ID="ddlResidente" runat="server" Class="mininos-input" placeholder="Roles">
                        </asp:DropDownList>




                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnEditar" runat="server" OnClick="btnEditar_Click" Text="Editar apadrinacion" Class="mininos-button-primary button"></asp:Button>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
