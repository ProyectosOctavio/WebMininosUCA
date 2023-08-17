<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfGestIncidentes.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestIncidentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Incidentes</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <a runat="server" href="~/wfNotificarIncidente" class="mininos-button-secondary button">Notificar Incidente</a>
            </div>
            <div class="col-full">
                <h1>Activas</h1>
                <div class="form-row-2">
                    <asp:TextBox ID="txtBusquedaActivas" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscarActivas" runat="server" Text="Buscar" OnClick="btnBuscarActivas_Click" Class="mininos-button-secondary" />
                </div>
                <br />
                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDatos_RowDataBound"
                    AllowPaging="true" OnPageIndexChanging="gvDatos_PageIndexChanging"
                    CssClass="table table-bordered table-condensed table-responsive table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="RESOLVER UN INCIDENTE" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="Sel" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NIVEL_RIESGO" HeaderText="NIVEL DE RIESGO" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                        <asp:BoundField DataField="RESIDENTE" HeaderText="RESIDENTE" />
                        <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" />
                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor=" #01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White"/>
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                </asp:GridView>
                <div style="text-align:left;">
                    <br />
                    <asp:Button ID="btnResolver" runat="server" Class="mininos-button-primary" Text="RESOLVER" OnClick="btnResolver_Click" />
                </div>
                <div>
                    <%--<asp:TextBox ID="txtIncidente" runat="server" Enabled="false"></asp:TextBox>--%>
                </div>
                <br />               
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-full">
                <h1>Historial</h1>
                <div class="form-row-2">
                    <asp:TextBox ID="txtBusquedaHistorial" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscarHistorial" runat="server" Text="Buscar" OnClick="btnBuscarHistorial_Click" Class="mininos-button-secondary" />
                </div>
                <br />
                <asp:GridView ID="gvHistorial" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvHistorial_RowDataBound"
                    AllowPaging="true" OnPageIndexChanging="gvHistorial_PageIndexChanging"
                    CssClass="table table-bordered table-condensed table-responsive table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="ELIMINAR UN INCIDENTE" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="SelH" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NIVEL_RIESGO" HeaderText="NIVEL DE RIESGO" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                        <asp:BoundField DataField="RESIDENTE" HeaderText="RESIDENTE" />
                        <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" />
                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor=" #01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White"/>
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                </asp:GridView>
                <div style="text-align:left;">
                    <br />                 
                    <asp:Button ID="btnEliminar" runat="server" Class="mininos-button-primary" Text="ELIMINAR" OnClick="btnEliminar_Click" />
                </div>
                <br />
                
            </div>
        </div>
        <asp:CustomValidator ID="cvDatos" runat="server" ForeColor="Red" Font-Size="Small"></asp:CustomValidator>
        <asp:ValidationSummary ID="vsPagina" runat="server" ForeColor="Red" Font-Size="Small" DisplayMode="List" />
    </div>
</asp:Content>
