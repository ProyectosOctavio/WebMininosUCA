<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfGestInfoBancaria.aspx.cs" EnableEventValidation="false" Inherits="ProyectoWebMininosUCA.wfGestInfoBancaria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css">

        <div class="banner-medium">
        <h1>Informacion bancaria</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Gestionar informacion bancaria</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_InfoBancaria" Visible="false" runat="server" />
            <div class="form-row-1">
             <asp:TextBox ID="txtBanco" runat="server" Class="mininos-input" placeholder="Banco"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtNumeroCuenta" runat="server" Class="mininos-input" placeholder="Numero cuenta"/>
            </div>
             <div class="form-row-1">
             <asp:TextBox ID="txtMoneda" runat="server" Class="mininos-input" placeholder="Moneda"/>
            </div>
        
                     <br />
    <asp:Button ID="btnRegistrarInfoBancaria" runat="server"   Class="mininos-button-primary"  Text="Agregar informacion bancaria" OnClick="btnGuardarInfoBancaria_Click"> </asp:Button>
                     <br />

                      <div class="col-half">
       
            <div class="mininos-form">

                <asp:GridView ID="gvInfoBancaria" runat="server" OnRowDataBound="gvInfoBancaria_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarInfoBancaria" OnClick="btnEditarInfoBancaria_Click" />
                                <asp:Button runat="server" Class="btn btn-primary" Text="Eliminar" ID="btnEliminarInfoBancaria" OnClick="btnEliminarInfoBancaria_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
                     </div>
                         </div>
       </div> 
                 
               
             </div>                
          </div>
    </asp:Content>
