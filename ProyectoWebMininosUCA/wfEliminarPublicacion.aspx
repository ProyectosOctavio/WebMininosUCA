<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEliminarPublicacion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarPublicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Eliminar Publicaciones</h1> 
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Eliminar Publicación</h1>
                    <div class="mininos-form">               

                        <asp:TextBox ID="txtIdPublicacionEliminar" Visible="false" runat="server" />             

                        <asp:Image ID="imgFotoEliminar" runat="server" Class="mininos-input" BorderColor="White"/>

                        <asp:TextBox ID="txtTituloPublicacionEliminar" runat="server" Placeholder="Título" Class="mininos-input" Enabled="false"/>

                        <asp:TextBox ID="txtTipoPublicacionEliminar" runat="server" Placeholder="Tipo" Class="mininos-input" Enabled="false"/>

                        <asp:TextBox ID="txtContenidoEliminar" TextMode="MultiLine" Wrap="true" runat="server" Placeholder="Descripción" Height="200px" Class="mininos-input" />



                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEliminarPublicacion" runat="server" Class="mininos-button-primary" Text="Eliminar Publicacion" OnClick="btnEliminarPublicacion_Click" ></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>