<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfGestPatologias.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestPatologias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


  
        <div class="banner-medium">
        <h1>Registrar patología</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Registrar una patología</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_patologia" Visible="false" runat="server" />
           
             <asp:TextBox ID="txtDescripcion" runat="server" Class="mininos-input" placeholder="Descripcion"/>
            
        </div>
                   
                 <br />
    <asp:Button ID="btnRegistrarPatologia" runat="server"   Class="mininos-button-primary"  Text="Registrar nueva Patología" OnClick="btnGuardarPatologia_Click"> </asp:Button>
                     <br />
                     <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="limpiarCampos_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>
                     <br />
                     <br />
                 </div> 
                 <br />
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
                 <div class="scrollable-gridview">
                    <div class="row">
                        <div class="row">
                            <div class="col-full">
                                <div class="table-responsive">

                <asp:GridView ID="gvPatologia" runat="server" OnRowDataBound="gvPatologia_RowDataBound"
                    CssClass="table table-bordered table-condensed table-responsive table-hover ">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" CssClass="btn btn-primary " Text="Editar" ID="btnEditarPatologia" OnClick="btnEditarPatologia_Click" />
                                <asp:Button runat="server" CssClass="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarPatologia" OnClick="btnEliminarPatologia_Click" />
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
       </div> 
    </asp:Content>
