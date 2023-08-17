<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfGestionarContacto.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestionarContacto" EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>CONTACTO</h1>
    </div>

    <asp:TextBox ID="txtId" Visible="false" runat="server" />

    <asp:TextBox ID="txtTelefono" Visible="false" runat="server" />

    <asp:TextBox ID="txtCorreo" Visible="false" runat="server" />

    <asp:TextBox ID="txtCorreo2" Visible="false" runat="server" />

    <asp:TextBox ID="txtTwitter" Visible="false" runat="server" />

    <asp:TextBox ID="txtInsta" Visible="false" runat="server" />
    
    <asp:TextBox ID="txtFacebook" Visible="false" runat="server" />


    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <h1>GESTIONAR CONTACTO</h1>
                <div class="mininos-form">
                    <asp:GridView ID="gvContacto" runat="server" OnRowDataBound="gvContacto_RowDataBound" CssClass="table table-bordered table-condensed table-responsive table-hover ">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarContacto" OnClick="btnEditarContacto_Click" />
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
</asp:Content>